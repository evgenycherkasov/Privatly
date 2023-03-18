import axios from "axios"
import https from 'https'


const baseUrl = "https://192.168.0.108:8443/api";

const httpsAgent = new https.Agent({
    rejectUnauthorized: false,
});

axios.defaults.httpsAgent = httpsAgent

export const createUser = async (telegramId) => {
    let response = await axios.post(`${baseUrl}/telegram_user/${telegramId}`, {
        telegramId: telegramId,
        username: telegramId
    });
    if (response.status === axios.HttpStatusCode.Ok) {
        return response.data;
    }

    return null;
}

export const getUser = async (userId) => {
    let response = await axios.get(`${baseUrl}/user/${userId}`, { validateStatus: false });

    if (response.status === axios.HttpStatusCode.Ok) {
        return response.data;
    }

    return null;
}

export const getUserId = async (telegramId) => {
    let response = await axios.get(`${baseUrl}/telegram_user/${telegramId}`, { validateStatus: false });

    if (response.status === axios.HttpStatusCode.Ok) {
        return response.data;
    }

    return null;
}

export const getSubscriptionPlans = async () => {
    let response = await axios.get(`${baseUrl}/subscriptionPlans`);
    if (response.status === axios.HttpStatusCode.Ok) {
        return response.data;
    }

    return null;
}

export const getPaymentUrl = async (userId, subscriptionPlanId, returnUrl) => {
    let response = await axios.get(`${baseUrl}/payment/create_payment/${userId}/${subscriptionPlanId}/${encodeURIComponent(returnUrl)}`);
    if (response.status === axios.HttpStatusCode.Ok) {
        return response.data;
    }

    return null;
}