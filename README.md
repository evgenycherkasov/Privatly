# Privatly
## _Hi✌🏻 Its Privatly!_

Privatly is a VPN service created with OpenVPN Server, .NET 7 and Node.js

## Installation

Privatly requires [Node.js](https://nodejs.org/) v16+ to run and [.NET](https://dotnet.microsoft.com/en-us/) 7.

To start Privatly API you can use:

```sh
docker compose build
docker compose up
```

For Privatly Telegram Bot:

```sh
cd src/telegramBot
npm i
TELEGRAM_BOT_TOKEN=<your_bot_token> node app.js
```