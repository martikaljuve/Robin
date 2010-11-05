void serialReceiverSetup() {
	
}

void serialReceiverLoop() {
	if (Serial.available()) {
		parseIncomingMessage();
	}
}

void parseIncomingMessage() {
	char command = Serial.read();
	switch(command) {
		case 'F':
			parseFireCommand();
			break;
		case 'M':
			parseMoveCommand();
			break;
		case 'T':
			parseTurnCommand();
			break;
		case 'S':
			parseStopCommand();
			break;
		case 'D':
			parseDribblerCommand();
			break;
		case 'G':
			parseMoveAndTurnCommand();
			break;
		case 'C':
			parseIrChannelCommand();
			break;
	}
}

void parseFireCommand() {
	byte power = Serial.read();

	//fireCoilgun(power);
}

void parseMoveCommand() {
	int dir = readIntFromSerial();
	int speed = readIntFromSerial();

	//move(dir, speed);
}

void parseTurnCommand() {
	int speed = readIntFromSerial();

	// turn(speed);
}

void parseStopCommand() {
	// stop();
}

void parseDribblerCommand() {
	byte enabled = Serial.read();

	// setDribbler(enabled == 1);
}

void parseMoveAndTurnCommand() {
	int direction = readIntFromSerial();
	int moveSpeed = readIntFromSerial();
	int turnSpeed = readIntFromSerial();

	// moveAndTurn(direction, moveSpeed, turnSpeed);
}

void parseIrChannelCommand() {
	byte channel = Serial.read();

	// setIrChannel(channel);
}