"use strict";
const { app, BrowserWindow } = require("electron");
const { join } = require("path");
process.env["ELECTRON_DISABLE_SECURITY_WARNINGS"] = "true";
const createWindow = () => {
  const win = new BrowserWindow({
    // 窗口图标
    icon: join(__dirname, "resource/favicon.ico"),
    width: 800,
    height: 600,
    webPreferences: {
      // contextIsolation: false,
      nodeIntegration: true,
      webSecurity: false
      // preload: path.join(__dirname, 'preload.js')
    }
  });
  if (process.env.VITE_DEV_SERVER_URL) {
    win.loadURL(process.env.VITE_DEV_SERVER_URL);
  } else {
    win.loadFile(join(__dirname, "dist/index.html"));
  }
};
app.whenReady().then(() => {
  createWindow();
  app.on("activate", () => {
    if (BrowserWindow.getAllWindows().length === 0)
      createWindow();
  });
});
app.on("window-all-closed", () => {
  if (process.platform !== "darwin")
    app.quit();
});
