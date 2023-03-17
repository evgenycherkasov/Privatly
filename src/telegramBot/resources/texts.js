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
        title : (userName, password, isSubscriptionActive, endDate) => `That's your account informationℹ️\n\nYour login: ${userName}\nYour password: ${password}\n\n${isSubscriptionActive ? `You have active subscription until ${endDate}👍` : 'You have not active subscription😔'}\n\nIf you lost some links or files you can press /download!`
    },

    faqMenu : {
        buttonText : "FAQ",
        title : "There are some answers..."
    },

    paymentMenu : {
        buttonText : (price, plan) => `${plan} | ${price} rubles`,
        title : (price, plan, description) => `You сhoose ${plan} option!\n\n${description}\n\nPrice of this subscription is ${price} rubles`
    },

    downloadCommand : {
        text : (isSubscriptionActive) => `${isSubscriptionActive ? 'You can download application for your laptop or PC by link below🧑‍💻\n\n dropbox link to desktop app\n\nAlso, you can use .ovpn configuration to setup your OpenVPN Connect application via smartphone' : 'Sorry but you have not active subscription😔'}`
    }
}
export default texts;