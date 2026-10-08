import { defineConfig } from '@playwright/test';
export default defineConfig({
  testDir: './tests/Consumers/Browser',
  testMatch: '*.spec.mjs',
  use: { baseURL: 'http://127.0.0.1:4173', browserName: 'chromium', trace: 'retain-on-failure' },
  webServer: { command: 'node tests/Consumers/Browser/server.mjs', url: 'http://127.0.0.1:4173', reuseExistingServer: false },
});
