union IntByteUnion {
	byte bytes[2];
	int number;
} bytesToInt;

int readIntFromSerial() {
	byte first = Serial.read();
	byte second = Serial.read();
	return getIntFromBytes(first, second);
}

int getIntFromBytes(byte first, byte second) {
	bytesToInt.bytes[0] = first;
	bytesToInt.bytes[1] = second;
	return bytesToInt.number;
}

byte* getBytesFromInt(int number) {
	bytesToInt.number = number;
	return bytesToInt.bytes;
}