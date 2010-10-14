<?php

/*
Program that calculates the table for sinuses and cosinuses.
*/

$percision = 5; //The percision of rounding. How many decimals after the coma.

//We calculate the values. Degrees from 0 to 359
for($i=0;$i<360;$i++){
	$sinuses[] = round(sin(deg2rad($i)), $percision);
	$cosinuses[] = round(cos(deg2rad($i)), $percision);
}

//For displaying a human-readable table
$th = '';
$td_sin = '';
$td_cos = '';

for($i=0;$i<360;$i++){
	$th .= '<th>'.$i.'</th>';
	$td_sin .= '<td>'.$sinuses[$i].'</td>';
	$td_cos .= '<td>'.$cosinuses[$i].'</td>';
}

//We display a human-readable table
echo '<table border=1>';
echo '<tr>'.$th.'</tr>';
echo '<tr>'.$td_sin.'</tr>';
echo '<tr>'.$td_cos.'</tr>';
echo '</table>';


echo '<br><br>';

//For creating array declaration commands for C/C++
$sinuses_str = '';
$cosinuses_str = '';

for($i=0;$i<360;$i++){
	$sinuses_str .= 'sinuses['.$i.'] = '.$sinuses[$i].';<br>';
	$cosinuses_str .= 'cosinuses['.$i.'] = '.$cosinuses[$i].';<br>';
}
echo 'float sinuses[360];<br>'.$sinuses_str;
echo 'float cosinuses[360];<br>'.$cosinuses_str;



?>