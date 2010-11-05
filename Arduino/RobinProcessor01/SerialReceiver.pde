int bufferIndex = 0;
int readerIndex = 0;
byte buffer[10];

void serialReceiverSetup() {
	
}

void serialReceiverLoop() {
	if (!Serial.available()) return;

	byte b = Serial.read();

	if (b == '.') { // HACK: Testime
		parseSerialBuffer();
		bufferIndex = 0;
	}
	else {
		buffer[bufferIndex] = b;
		bufferIndex++;
	}

	if (bufferIndex >= 10) {
		bufferIndex = 0;
	}
}

void parseSerialBuffer() {
	if (bufferIndex == 0) return;

	readerIndex = 0;
	char command = readByteFromBuffer();

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

	//Serial.flush();
}

byte readByteFromBuffer() {
	byte value = buffer[readerIndex];
	readerIndex++;
	return value;
}

int readIntFromBuffer() {
	int value = getIntFromBytes(buffer[readerIndex], buffer[readerIndex+1]);
	readerIndex += 2;
	return value;
}

void parseFireCommand() {
	byte power = readByteFromBuffer();

	//fireCoilgun(power);
}

void parseMoveCommand() {
	int dir = readIntFromBuffer();
	int speed = readIntFromBuffer();

	Serial.print("Move dir: ");
	Serial.print(dir);
	Serial.print(", speed: ");
	Serial.println(speed);

	MotorBoard::sendCommand('M', dir, speed);
}

void parseTurnCommand() {
	int speed = readIntFromBuffer();

	Serial.print("Turn speed: ");
	Serial.println(speed);

	MotorBoard::sendCommand('T', speed);
}

void parseStopCommand() {
	MotorBoard::sendCommand('S');
}

void parseDribblerCommand() {
	byte enabled = readByteFromBuffer();

	if ((enabled & 1) == 1)
		MotorBoard::sendCommand('D');
	else
		MotorBoard::sendCommand('d');
}

void parseMoveAndTurnCommand() {
	int direction = readIntFromBuffer();
	int moveSpeed = readIntFromBuffer();
	int turnSpeed = readIntFromBuffer();

	MotorBoard::sendCommand('G', direction, moveSpeed, turnSpeed);
}

void parseIrChannelCommand() {
	byte channel = readByteFromBuffer();

	// setIrChannel(channel);
}