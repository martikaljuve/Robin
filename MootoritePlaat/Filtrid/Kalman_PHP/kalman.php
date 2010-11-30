<?php

/*
Kalmani filter
Raimond Tunnel
28. november 2010

Kood rakendab Kalmani filtrit lineaarselt muutuvale suurusele määratud müraastmega. Suurusele genereeritakse igas punktis juhuslikku müra etteantud piirides.
Programmi saab jooksutada nii vastava graafiku genereerimiseks kui ka konsooliaknas tabelina tulemuste väljastamiseks.
Kuna programm rakendab filtrit ainult ühele suurusele ühest allikast, siis maatriksid on 1x1 ehk neid saab vaadelda tavaliste reaalarvudena.

Abiks:
"Kalman Filter For Dummies", http://bilgin.esme.org/BitsBytes/KalmanFilterforDummies.aspx
"Final version of ArduIMU (Include Video)", http://diydrones.com/profiles/blog/show?id=705844%3ABlogPost%3A23188 [vt. ka source'i]
"Kalman Filtering ", http://www.innovatia.com/software/papers/kalman.htm [vt. ka Math PDF-i]
"Kalman filter", http://en.wikipedia.org/wiki/Kalman_filter
*/

$displayImage = true; //Kui true, siis väljastame graafiku; false korral on soovitav konsoolis käivitada

//Algväärtustused
$measuredValue = 0;
$currentEstimation = 0;


$currentP = 1; //"prior error covariance"

//H = 1;
//A = 1
//x_k = A*x_(k-1) + B*controlSignal (=0) + w_(k-1) processNoise


$measurementNoiseConst = 50; //Signaali müra
$measurementNoise = $measurementNoiseConst; //v_k, R


//See mõjutab seda, kui palju mõõtmistulemused filtri tulemusi mõjutavad. Liiga väikse väärtuse korral jääme mõõtmistulemustest liiga maha ja liiga suure korral kandub müra edasi. Katsetada erinevate väärtuste korral.
$processNoise = 2; //w_k, Q



for($i=1;$i<100;$i++){ //Teeme 100 iteratsiooni

	//Prediction
	//$currentEstimation = $previousEstimation;
	//$currentP = $previousP;
	$currentP = $currentP + $Q;


	$measuredValue = $i+rand(-$measurementNoiseConst, $measurementNoiseConst); //Signal value + measurement noise 
	// z_k = H*x_k + v_k, H=1


	//Correction
	$kalmanGain = $currentP / ($currentP + $measurementNoise); //Arvutame Kalmani gain'i
	$currentEstimation = $currentEstimation + $kalmanGain * ($measuredValue - $currentEstimation); //Praegune hinnang
	$currentP = (1-$kalmanGain)*$currentP + $processNoise; //Arvutame uue veasuhte


	$results[$resultIndex++] = array('measuredValue' => $measuredValue, 'currentEstimation' => round($currentEstimation), 'kalmanGain' => $kalmanGain); //Kirjutame tulemused massiivi


}
if(!$displayImage){
	foreach($results as $result){
		echo $result['measuredValue']."\t".$result['currentEstimation']."\t".$result['kalmanGain']."\n";
	}
}else{
	drawImage();
}

function drawImage(){
	global $results;

	$width = 500;
	$height = 400;
	$im = imagecreatetruecolor($width,$height);

	$must = imagecolorallocate($im, 0, 0, 0);
	$punane = imagecolorallocatealpha($im, 200, 0, 0, 20);
	$sinine = imagecolorallocatealpha($im, 0, 0, 200, 20);
	$helesinine = imagecolorallocatealpha($im, 40, 140, 250, 80);

	$horisontalLineY = 150;
	$unitWidth = ($width-50) / count($results);

	//Parempoolne "telg"
	for($y = -60; $y < 160; $y+=20){
		imagestring($im, 3, $width-30, $y+$horisontalLineY, $y, $helesinine);
	}

	//Graafikud
	foreach($results as $resultIndex => $result){
		if($resultIndex < count($results)-1){

			imageline($im, $resultIndex*$unitWidth, $horisontalLineY + $result['measuredValue'], ($resultIndex+1)*$unitWidth , $horisontalLineY + $results[$resultIndex+1]['measuredValue'], $punane);
			imageline($im, $resultIndex*$unitWidth, $horisontalLineY + $result['currentEstimation'], ($resultIndex+1)*$unitWidth , $horisontalLineY + $results[$resultIndex+1]['currentEstimation'], $sinine);


		}
	}

	//Legend
	imagestring($im, 3, 10, 10, 'Kalmani filter ühemõõtmelise üksiku suuruse peal', $helesinine);

	imageline($im, 10, $height-40, 30, $height-40, $punane);
	imagestring($im, 3, 40, $height-45, 'Mõõtmistulemus', $punane);

	imageline($im, 10, $height-20, 30, $height-20, $sinine);
	imagestring($im, 3, 40, $height-25, 'Kalmani filter', $sinine);

	header('Content-type:image/png');
	imagepng($im);

}



?>