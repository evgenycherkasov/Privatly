import { getTelegramBot, startup } from "./bot.js";
import apiService from "./services/apiService.js";
import rabbitMq from "./services/rabbitMqService.js";

import EventEmitter from "events";


async function main() {
    console.log("Starting bot application Privatly");
    const eventEmitter = new EventEmitter();
    console.log("Event emitter initailized");
    const bot = await getTelegramBot(process.env.TELEGRAM_BOT_TOKEN, apiService, eventEmitter);
    console.log("Telegram bot object initialized");
    const rabbitMqService = await rabbitMq.initializeConsumer(eventEmitter);
    console.log("RabbitMQ service started");
    console.log("Start telegram bot with token: ", bot.telegram.token);
    await startup(bot);
}

main();