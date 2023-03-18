import { Telegraf } from 'telegraf'
import { MenuTemplate, MenuMiddleware, createBackMainMenuButtons } from 'telegraf-inline-menu'
import texts from "./resources/texts.js"

export async function getTelegramBot(token, apiService) {
    const plans = await apiService.getSubscriptionPlans();

    const plansNames = plans.map(p => p.name);

    const mainMenu = new MenuTemplate(texts.mainMenu.title);

    const paymentMenu = new MenuTemplate(async ctx => {
        let option = plans.filter(p => p.name === ctx.match[1]);
        return texts.paymentMenu.title(option[0].price, option[0].name, option[0].description);
    });
    paymentMenu.url(ctx => {
        let option = plans.filter(p => p.name === ctx.match[1]);
        return texts.paymentMenu.buttonText(option[0].price, option[0].name)
    }, async ctx => {
        let planId = plans.filter(p => p.name === ctx.match[1])[0].id;
        let userId = await apiService.getUserId(ctx.from.id);
        return await apiService.getPaymentUrl(userId, planId);
    });

    const letsGoMenu = new MenuTemplate(texts.letsGoMenu.title);
    letsGoMenu.chooseIntoSubmenu('paymentOption', plansNames, paymentMenu)

    const accountMenu = new MenuTemplate(async ctx => {
        let userId = await apiService.getUserId(ctx.from.id);
        let user = await apiService.getUser(userId);

        let subscriptionEndDate = Date.parse(user.subscriptionEndDate);

        let isSubscriptionActive = subscriptionEndDate > Date.now();

        return texts.accountMenu.title(user.login, user.password, isSubscriptionActive, new Date(subscriptionEndDate).toDateString());
    });

    const faqMenu = new MenuTemplate(texts.faqMenu.title);

    letsGoMenu.manualRow(createBackMainMenuButtons(texts.backButtons.backButtonText, texts.backButtons.mainMenuText));
    accountMenu.manualRow(createBackMainMenuButtons(texts.backButtons.backButtonText, texts.backButtons.mainMenuText));
    faqMenu.manualRow(createBackMainMenuButtons(texts.backButtons.backButtonText, texts.backButtons.mainMenuText));
    paymentMenu.manualRow(createBackMainMenuButtons(texts.backButtons.backButtonText, texts.backButtons.mainMenuText));

    mainMenu.submenu(texts.letsGoMenu.buttonText, "letsgo", letsGoMenu);
    mainMenu.submenu(texts.accountMenu.buttonText, "account", accountMenu);
    mainMenu.submenu(texts.faqMenu.buttonText, "faq", faqMenu);

    const bot = new Telegraf(token);

    const menuMiddleware = new MenuMiddleware('/', mainMenu)

    bot.command('start', async ctx => {
        let userId = await apiService.getUserId(ctx.from.id);

        if (userId === null) {
            await apiService.createUser(ctx.from.id);
        }

        return menuMiddleware.replyToContext(ctx)
    });

    bot.command('download', async ctx => {
        let userId = await apiService.getUserId(ctx.from.id);
        let user = await apiService.getUser(userId);

        let subscriptionEndDate = Date.parse(user.subscriptionEndDate);
        let isSubscriptionActive = subscriptionEndDate > Date.now();

        await ctx.reply(texts.downloadCommand.text(isSubscriptionActive));
    });

    bot.use(menuMiddleware.middleware())

    bot.catch(error => {
        console.log('bot error', error)
    })

    return bot;
}

export async function startup(bot) {
    await bot.launch();
}

export async function sendOnSuccessfulPayment(telegramId) {
    
}