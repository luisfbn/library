import axios from 'axios';
import { jwtDecode } from 'jwt-decode';

// const BASE_URL = 'http://localhost:5190/api';
const BASE_URL = 'https://localhost:7120/api';

export const authenticate = async (username: string, password: string) => {
    try {
        const response = await axios.post(`${BASE_URL}/user/authenticate`, {
        username,
        password,
        });

        const data = response.data;

        localStorage.setItem('token', data.token);

        return data;

    } catch (error) {
        console.error('Error:', error);
        throw error;
    }
}


export const logout = () => {
    localStorage.removeItem('token');
}


export const get = () => {
    const token = localStorage.getItem("token");
    if (token) {
        const decodedToken = jwtDecode(token);
        return (decodedToken);
    }
    return null;
}
