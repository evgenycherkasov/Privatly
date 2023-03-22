const texts = {
    mainMenu : {
        title : "Hi✌🏻 Its Privatly!\n\nWe will provide you with an excellent solution to your problems with access to blocked sites with best prices🖥"
    },

    backButtons : {
        backButtonText : "🔙",
        mainMenuText : "Main menu🔙"
    },

    letsGoMenu : {
        buttonText : "Select subscription",
        title : "Our VPN are stable fast reliable and secure!\n\nJust choose the most suitable option for you and start using!"
    },

    accountMenu : {
        buttonText : "My account",
        title : (userName, password, isSubscriptionActive, endDate) => `That's your account informationℹ️\n\nYour login: ${userName}\nYour password: ${password}\n\n${isSubscriptionActive ? `You have active subscription until ${endDate}👍` : 'You have not active subscription😔'}\n\nIf you lost some links or files you can press /download!\n\nIf you need help installing Privatly VPN you can use the /help command!`
    },

    paymentMenu : {
        buttonText : (price, plan) => `${plan} | ${price} rubles`,
        title : (price, plan, description) => `You сhoose ${plan} option!\n\n${description}\n\nPrice of this subscription is ${price} rubles`
    },

    downloadCommand : {
        ovpnText: "That is your .ovpn configuration for connect to Privatly VPN!",
        text : (isSubscriptionActive, link) => `${isSubscriptionActive ? `You can download application for your laptop or PC by link below🧑‍💻\n\nThat is your link for desktop application: ${link}\n\nAlso, you can use .ovpn configuration to setup your OpenVPN Connect application via smartphone\n\nIf you need help installing Privatly VPN you can use the /help command!` : 'Sorry but you have not active subscription😔'}`
    },

    successPayment : {
        text : (login, password, endDate) => `***Congratulations!***\n\nYou have renewed your subscription until ${endDate}\n\nThat's your account informationℹ️\n\nYour login: ${login}\nYour password: ${password}\n\nYou can press /download to download your VPN application or .ovpn configuration!\n\nIf you need help installing Privatly VPN you can use the /help command!`
    },

    helpCommand : {
        mobileTutorial : "That is your tutorial to setup Privatly VPN in your smartphone!",
        text : "To set up Privatly VPN you can watch our video tutorial\n\nInstall OpenVPN Connect app firstly\n\niOS: https://apps.apple.com/ru/app/openvpn-connect/id590379981\nAndroid: https://play.google.com/store/apps/details?id=net.openvpn.openvpn&hl=ru&gl=US&pli=1\n\nIf you have any questions, you can contact our support @evgrcg"
    }
}
export default texts;