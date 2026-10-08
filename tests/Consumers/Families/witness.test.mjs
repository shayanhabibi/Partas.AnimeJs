import { test } from 'node:test';
import assert from 'node:assert/strict';
import * as witness from './output/Witness.fs.js';
import { snap } from '../../../node_modules/animejs/dist/modules/core/helpers.js';
test('timeline defaults and ordered actions animate two targets', () => {
  assert.equal(witness.firstAtStart, 0);
  assert.equal(witness.firstAtEnd, 100);
  assert.equal(witness.secondAtEnd, 200);
});
test('numeric arrays returned by typed callbacks work with AnimeJs snapping', () => {
  const options = witness.callbackArrayOptions;
  assert.equal(snap(70, options.snap({})), 100);
  for (const name of ['snap', 'container', 'containerPadding']) {
    assert.equal(Array.isArray(options[name]({})), true, name);
    const first = options[name]({});
    first[0] = 999;
    assert.notEqual(options[name]({})[0], 999);
  }
  assert.equal(snap(70, witness.axisOptions.snap({})), 100);
  assert.equal(witness.receiverOptions.snap.length, 1);
  assert.deepEqual(witness.receiverOptions.snap.call({ offset: 3 }, { offset: 7 }), [3, 7]);
});
test('duration and percentage keyframes retain distinct shapes', () => {
  assert.deepEqual(Array.from(witness.materializedOrder), [0, 50, 100]);
  assert.equal(witness.percentageMiddle, 50);
  assert.equal(witness.durationEnd, 100);
});
test('stagger delegate and timer callbacks use real AnimeJs', () => {
  assert.equal(witness.staggerFirst, 0);
  assert.equal(witness.staggerSecond, 25);
  assert.equal(witness.timerUpdated, true);
});
test('default Fable numeric arrays materialize as independent plain arrays', () => {
  assert.equal(Array.isArray(witness.gridOne.grid), true);
  witness.gridOne.grid[0] = 99;
  assert.equal(witness.gridTwo.grid[0], 2);
});
test('percentage positions and reserved dynamic keys are validated', () => {
  for (const value of [NaN, Infinity, -1, 101]) assert.throws(() => witness.percentagePosition(value));
  for (const key of ['duration', '__proto__', 'constructor', 'prototype']) assert.throws(() => witness.customProperty(key));
});
