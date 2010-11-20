class SerialHelper {
public:
	byte readByte() {
		if (Serial.available() < 1)
			return 0;

		return Serial.read();
	}

	int readInt() {
		if (Serial.available() < 2)
			return 0;

		byte first = Serial.read();
		byte second = Serial.read();
		return (second << 8) + first;
	}

	void writeInt(int value) {
		Serial.write(value & 255);
		Serial.write(value >> 8);
		//Serial.write(lowByte(value));
		//Serial.write(highByte(value));
	}
};

SerialHelper SerialUtil;