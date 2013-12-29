describe("when going to a state and there is a current state", function() {

	var group = Bifrost.interaction.VisualStateGroup.create();

	var firstState = {
		name: "something Else",
		exit: sinon.stub()
	};

	var secondState = {
		name : "something",
		enter: sinon.stub()
	};

	var namingRoot = { some: "namingRoot" };

	group.defaultDuration = Bifrost.TimeSpan.fromMilliseconds(100);
	group.states.push(firstState);
	group.states.push(secondState);

	group.currentState(firstState);
	group.goTo(namingRoot, "something");

	it("should switch current state", function() {
		expect(group.currentState()).toBe(secondState);
	});

	it("should tell the current state to exit", function() {
		expect(firstState.exit.calledWith(namingRoot, group.defaultDuration)).toBe(true);
	});

	it("should tell the state to enter", function() {
		expect(secondState.enter.calledWith(namingRoot, group.defaultDuration)).toBe(true);
	});
});