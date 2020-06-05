import { BrowserWindow, App, app } from 'electron';
const path = require("path");
const url = require("url");

export default class Main {
    static mainWindow: BrowserWindow;
    static application: App;
    static BrowserWindow;

    private static onWindowAllClosed() {
        if (process.platform !== 'darwin') {
            Main.application.quit();
        }
    }

    private static onClose() {
        Main.mainWindow = null;
    }

    private static onReady() {
        Main.mainWindow = new Main.BrowserWindow({ width: 800, height: 600 });
        Main.mainWindow.loadURL(url.format({
            pathname: path.join(__dirname, "/dist/index.html"),
            protocol: "file:",
            slashes: true
        }));
        Main.mainWindow.on('closed', Main.onClose);
    }

    static main(app: Electron.App, browserWindow: typeof BrowserWindow) {
        Main.BrowserWindow = browserWindow;
        Main.application = app;
        Main.application.on('window-all-closed', Main.onWindowAllClosed);
        Main.application.on('ready', Main.onReady);
    }
}

Main.main(app, BrowserWindow);
