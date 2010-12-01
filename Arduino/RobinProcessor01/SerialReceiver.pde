byte buffer[10];

void serialReceiverSetup() {
}

void serialReceiverLoop() {
	if(Serial.available() < 9)
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
			if (!POWER) break;
			parseMoveCommand();
			break;
		case 'T':
			if (!POWER) break;
			parseTurnCommand();
			break;
		case 'S':
			parseStopCommand();
			break;
		case 'D':
			parseDribblerCommand();
			break;
		case 'G':
			if (!POWER) break;
			parseMoveAndTurnCommand();
			break;
		case 'C':
			parseIrChannelCommand();
			break;
		case 'X':
			parseExtraCommand();
			break;
		default:
			break;
	}

	// Read until \r\n or end
	byte previous = 0;
	while (Serial.available() > 0) {
		byte current = SerialUtil.readByte();
		if (previous == '\r' && current == '\n')
			break;
		previous = current;
	}
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
	int speed = SerialUtil.readInt();

	MotorBoard::sendCommand('D', speed);
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
