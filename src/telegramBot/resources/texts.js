const texts = {
    mainMenu : {
        title : "Hi✌🏻 Its Privatly!\n\nWe will provide you with an excellent solution to your problems with access to blocked sites with best prices🖥\n\nLeeeeet's goooo🔥"
    },

    backButtons : {
        backButtonText : "🔙",
        mainMenuText : "Main menu🔙"
    },

    letsGoMenu : {
        buttonText : "Leeeeet's goooo🔥",
        title : "Our VPN are stable🧑‍💻 fast⏫ reliable🏰 and secure🔐\n\nJust choose the most suitable option for you and start using!"
    },

    accountMenu : {
        buttonText : "My account",
        title : (userName, password, isSubscriptionActive, endDate) => `That's your account informationℹ️\n\nYour login: ${userName}\nYour password: ${password}\n\n${isSubscriptionActive ? `You have active subscription untill ${endDate}👍` : 'You have not active subscription😔'}`
    },

    faqMenu : {
        buttonText : "FAQ",
        title : "There are some answers..."
    },

    paymentMenu : {
        buttonText : (price, plan) => `${plan} | ${price} rubles`,
        title : (price, plan, description) => `You сhoose ${plan} option!\n\n${description}\n\nPrice of this subscription is ${price} rubles`
    }
}
export default texts;