import { test } from 'node:test';
import assert from 'node:assert/strict';
import { initial, final, correctHandle } from './output/Witness.fs.js';

test('shipped library animates a real object and returns the AnimeJs handle', () => {
  assert.equal(initial, 0);
  assert.equal(final, 100);
  assert.equal(correctHandle, true);
});
