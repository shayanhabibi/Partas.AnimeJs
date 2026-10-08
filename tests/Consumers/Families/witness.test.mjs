import { test } from 'node:test';
import assert from 'node:assert/strict';
import * as witness from './output/Witness.fs.js';
test('timeline defaults and ordered actions animate two targets', () => {
  assert.equal(witness.firstAtStart, 0);
  assert.equal(witness.firstAtEnd, 100);
  assert.equal(witness.secondAtEnd, 200);
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
