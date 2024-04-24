import axios from 'axios';

// const BASE_URL = 'http://localhost:5190/api';
const BASE_URL = 'https://localhost:7120/api';

export const GetAll = async () => {
  try {
      const response = await axios.get(`${BASE_URL}/readers`);
      return response.data;

  } catch (error) {
      console.error(error);
      throw error;
  }
}

