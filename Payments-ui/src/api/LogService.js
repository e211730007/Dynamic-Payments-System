import axios from 'axios';

const BASE_URL = 'http://localhost:5193/api';

export const getLogs = async () => {
    try {
        const response = await axios.get(`${BASE_URL}/Logs`);
        return response.data;
    } catch (error) {
        console.error("Error fetching logs:", error);
        return [];
    }
};

export const runNewCalculation = async (methodName) => {
    try {
        const response = await axios.post(`${BASE_URL}/calculate`, { method: methodName });
        return response.data;
    } catch (error) {
        console.error("Error running calculation:", error);
        throw error;
    }
};