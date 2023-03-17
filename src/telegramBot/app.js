import { getTelegramBot, startup } from "./bot.js";
import apiService from "./services/apiService.js";

async function main() {
    const bot = await getTelegramBot("1280227143:AAHuMQD-oHAJasmihRKcVJyUyRiIla5RtVg", apiService);
    await startup(bot);
}

main();