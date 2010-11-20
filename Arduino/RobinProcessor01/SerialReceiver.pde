byte buffer[10];

void serialReceiverSetup() {
}

void serialReceiverLoop() {
	if(Serial.available() < 8)
		return;
	
	parseSerialBuffer();
}

void parseSerialBuffer() {
	char command = SerialUtil.readByte();

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
		case 'X':
			parseExtraCommand();
			break;
	}

	//Serial.flush();
}

void parseFireCommand() {
	byte power = SerialUtil.readByte();

	fireCoilgun(power);
}

void parseMoveCommand() {
	int dir = SerialUtil.readInt();
	int speed = SerialUtil.readInt();

	MotorBoard::sendCommand('M', dir, speed);
}

void parseTurnCommand() {
	int speed = SerialUtil.readInt();

	MotorBoard::sendCommand('T', speed);
}

void parseStopCommand() {
	MotorBoard::sendCommand('S');
}

void parseDribblerCommand() {
	byte enabled = SerialUtil.readByte();

	if ((enabled & 1) == 1)
		MotorBoard::sendCommand('D');
	else
		MotorBoard::sendCommand('d');
}

void parseMoveAndTurnCommand() {
	int direction = SerialUtil.readInt();
	int moveSpeed = SerialUtil.readInt();
	int turnSpeed = SerialUtil.readInt();
	
	MotorBoard::sendCommand('G', direction, moveSpeed, turnSpeed);
}

void parseIrChannelCommand() {
	byte channel = SerialUtil.readByte();

	setIrChannel(channel);
}

void parseExtraCommand() {
	byte extra = SerialUtil.readByte();

	setLedRed(getBit(extra, 1));
	setLedGreen(getBit(extra, 2));
	setLedBlue(getBit(extra, 3));
}