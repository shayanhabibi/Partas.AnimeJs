import assert from 'node:assert/strict';
import { test } from 'node:test';
import { duplicate, absent, explicitNull, first, second, empty, fromSequence, fromOptions, loops, ordered, executionTrace } from './output/Options.fs.js';

test('last field wins', () => assert.equal(duplicate.duration, 200));
test('absence differs from null', () => {
  assert.equal(Object.hasOwn(absent, 'onComplete'), false);
  assert.equal(explicitNull.onComplete, null);
});
test('reusable options produce independent plain objects', () => {
  assert.equal(first.duration, 100);
  first.duration = 999;
  assert.equal(second.duration, 100);
  assert.equal(Object.getPrototypeOf(second), Object.prototype);
});
test('zero, sequences, options, and loops preserve accumulation', () => {
  assert.deepEqual(empty, {});
  assert.equal(fromSequence.duration, 75);
  assert.equal(fromOptions.duration, 150);
  assert.equal(loops.duration, 100);
});
test('arrays and evaluation remain in source order', () => {
  assert.deepEqual(ordered.keyframes, [0, 50, 100]);
  assert.deepEqual(executionTrace, [0, 50, 100]);
});
