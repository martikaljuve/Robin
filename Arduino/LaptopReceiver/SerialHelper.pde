union IntByteUnion {
	byte bytes[2];
	int number;
} bytesToInt;

int readIntFromSerial() {
	return getIntFromBytes(Serial.read(), Serial.read());
}

int getIntFromBytes(byte first, byte second) {
	bytesToInt.bytes[0] = first;
	bytesToInt.bytes[1] = second;
	return bytesToInt.number;
}

byte[] getBytesFromInt(int number) {
	bytesToInt.number = number;
	return bytesToInt.bytes;
}