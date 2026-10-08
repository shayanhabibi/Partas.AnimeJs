import assert from 'node:assert/strict';
import { test } from 'node:test';
import { createPlainObjectWitness, materialized, rejectedReservedKey } from './output/Witness.fs.js';

test('real AnimeJs changes a plain object deterministically after explicit factory call', () => {
  const witness = createPlainObjectWitness();
  assert.equal(witness.before, 0);
  assert.equal(witness.afterOptions, 0);
  assert.equal(witness.afterSeek, 100);
  assert.equal(witness.completed, true);
  assert.equal(witness.correctHandle, true);
});
test('DSL objects preserve keys, ordinary arrays, callbacks, and tween values', () => {
  const options = materialized();
  assert.equal(options.duration, 100);
  assert.equal(options.ease, 'linear');
  assert.equal(options.autoplay, false);
  assert.deepEqual(options.opacity, [0, 1]);
  assert.deepEqual(options.x, { from: 0, to: 100 });
  assert.equal(options.onComplete.length, 1);
  assert.equal(options.onComplete({}), undefined);
});
test('custom property escape rejects reserved option keys', () => assert.throws(rejectedReservedKey, /Custom keys/));
