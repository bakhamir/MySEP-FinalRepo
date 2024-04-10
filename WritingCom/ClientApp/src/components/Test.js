import React, { Component } from 'react';

export class Test extends Component {
  static displayName = Test.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 9, message: "undef"};
      this.incrementCounter = this.incrementCounter.bind(this);
      this.HelloWorlder = this.HelloWorlder.bind(this);
  }

  incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount - 1
    });
    }
    HelloWorlder() {
        this.setState({
            message: "Hello world!"
        });
    }

  render() {
    return (
      <div>
        <h1>Counter</h1>

        <p>This is a simple example of a React component.</p>

        <p aria-live="polite">Current count: <strong>{this.state.currentCount}</strong></p>

            <p> {this.message} </p>

        <button className="btn btn-primary" onClick={this.incrementCounter}>Decrement</button>
            <button className="btn btn-primary" onClick={this.HelloWorlder}>Decrement</button>
      </div>
    );
  }
}
