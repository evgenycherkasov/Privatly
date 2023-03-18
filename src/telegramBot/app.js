import { getTelegramBot, startup } from "./bot.js";
import apiService from "./services/apiService.js";
import rabbitMq from "./services/rabbitMqService.js";

async function main() {
    const bot = await getTelegramBot("1280227143:AAHuMQD-oHAJasmihRKcVJyUyRiIla5RtVg", apiService);
    const rabbitMqService = await rabbitMq.initializeConsumer(bot, apiService);
    await startup(bot);
}

main();