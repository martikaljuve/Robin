
class MovementPlanner {
	int speedLeft, speedRight, speedBack;
	static const double rpmToTenthDegreesPerMillisecond = 0.06; // 3600 / 60s / 1000ms

public:
	long positionLeft;
	long positionRight;
	long positionBack;

	void move(int direction, int speed);
	void turn(int speed);
	void moveAndTurn(int direction, int moveSpeed, int turnSpeed);
	void stop();

	void setMovement(int leftRpm, int rightRpm, int backRpm);
	void update(unsigned long deltaInMilliseconds);

private:
	void moveAndTurnCalculate(int direction, int moveSpeed, int turnSpeed, int &left, int &right, int &back);
};

const double MovementPlanner::rpmToTenthDegreesPerMillisecond;
