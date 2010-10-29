class AngleUtil {
	long prev;
	long diff;
	unsigned long previousTime;

public:
	float speed;

	AngleUtil() { }
	
	void add(int angle) {
		if (angle < 0) return;
		
		int delta = angle - prev;
		if (delta < -1800) {
			diff += 3600 + delta;
		}
		else if (delta > 1800) {
			diff += delta - 3600;
		}
		else {
			diff += delta;
		}

		prev = angle;
	}
	
	void calculateSpeed(long currentTime) {
		unsigned int timeDiff = currentTime - previousTime;
		
		if (timeDiff <= 0) return;
		if (diff <= 0) return;
		
		speed = (diff / (float)timeDiff) * 16.666667; // 60000ms / 360.0 (3600) degrees
		
		previousTime = currentTime;
		diff = 0;
	}
};