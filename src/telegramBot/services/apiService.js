import {getSubscriptionPlans, getPaymentUrl, getUserId, createUser, getUser} from "../api/privatlyApiInteraction.js";

const returnUrl = "https://t.me/privatlybot";

const apiService = {
    createUser : async (telegramId) => {
        const response = await createUser(telegramId);
        return response;
    },

    getUserId : async (telegramId) => {
        const response = await getUserId(telegramId);
        return response;
    },

    getUser : async (userId) => {
        const response = await getUser(userId);
        return response;
    },

    getSubscriptionPlans : async () => {
        const response = await getSubscriptionPlans();
        return response;
    },

    getPaymentUrl : async (userId, subscriptionPlanId) => {
        const response = await getPaymentUrl(userId, subscriptionPlanId, returnUrl);
        return response;
    }
}

export default apiService;