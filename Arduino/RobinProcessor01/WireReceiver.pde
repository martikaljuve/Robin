TimedAction wireRequestAction = TimedAction(100, wireRequest);

CommandData command;
union CommandUnion cmdUnion;

void wireReceiverSetup() {

}

void wireReceiverLoop() {
	wireRequestAction.check();
}

void wireRequest() {
	Wire.requestFrom(1, 7);

	int available = min(7, Wire.available());
	for(int i = 0; i < available; i++) {
		cmdUnion.bytes[i] = Wire.receive();
	}

	parseWireCommand(cmdUnion.command);
}

void parseWireCommand(CommandData cmd) {
	char command = cmd.command;

	switch(command) {
	case 'P':
		globalX = cmd.first;
		globalY = cmd.second;
		globalDirection = cmd.third;
		break;
	default:
		break;
	}
}
