#include <FiniteStateMachine.h>

State Running = State(running);
State ClosingInOnNearbyTarget = State(closingInOnNearbyTarget);
State CarryingBall = State(carryingBall);

FSM stateMachine = FSM(Running);

bool tripSensorOn = false;
bool leftSonarProximity = false;
bool rightSonarProximity = false;

void stateMachineSetup() {
	
}

void stateMachineLoop() {
	stateMachine.update();
}

void running() {
	if (tripSensorOn == true) {
		stateMachine.transitionTo(CarryingBall);
		return;
	}
	if (leftSonarProximity || rightSonarProximity) {
		stateMachine.transitionTo(ClosingInOnNearbyTarget);
		return;
	}
}

void closingInOnNearbyTarget() {
	if (tripSensorOn) {
		stateMachine.transitionTo(CarryingBall);
		return;
	}
}

void carryingBall() {
	if (tripSensorOn == false) {
		stateMachine.transitionTo(Running);
		return;
	}
}