// =====================================================================
// Wiring
//
// This class encapsulates communication with an Wiring I/O
// board. Subclasses handle the details of a specific serial protocol.
//
// When a message arrives (according to the protocol), the 'action'
// instance variable is invoked, with the expanded message contents.
//
// Messages are sent to the board with the 'send' method. it takes any
// number of arguments, the meaning of which again are again protocol
// specific.

// =====================================================================
// basic_communication (from the Messenger distribution)
// see http://www.arduino.cc/playground/Code/Messenger

p = WiringSMS("/dev/tty.usbserial-A20022bM", 115200); 
p.action = { |... msg| msg.postln };

// read digital inputs
p.send($r, $d);

// read analog inputs
p.send($r, $a);

// write digital output 48
p.send($w, $d, 48, 1);
p.send($w, $d, 48, 0);

// flash LED on digital pin 48
(
fork {
	8.do {
		p.send($w, $d, 48, 1);
		1.25.wait;
		p.send($w, $d, 48, 0);
		0.25.wait;
	}
}
)

// write analog output 0
p.send($w, $a, 0, 128);

// close port
p.close;

// =====================================================================
// analog_read

p = WiringSMS("/dev/tty.usbserial-A20022bM", 115200);
p.action = { |... msg| msg.postln };

// thor reading loop
(
fork {
	100.do {
		p.send($r, $a);
		0.1.wait;
	}
}
)

p.close;

// EOF
