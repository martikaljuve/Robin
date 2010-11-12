class SerialHelper {
public:
	byte readByte() {
		return Serial.read();
	}

	int readInt() {
		byte first = Serial.read();
		byte second = Serial.read();
		return (second << 8) + first;
	}

	void writeInt(int value) {
		Serial.write(lowByte(value));
		Serial.write(highByte(value));
	}
};

SerialHelper SerialUtil;