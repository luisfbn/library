import axios from 'axios';

// const BASE_URL = 'http://localhost:5190/api';
const BASE_URL = 'https://localhost:7120/api';


export const Add = async(bookData: any)  => {
  try {
    
      const response = await axios.post(`${BASE_URL}/books`, bookData);
      return response.data;

  } catch (error: any) {
      console.error(error);
      throw error;
  }
}

export const Delete = async (bookId: string) => {
  try {

      const response = await axios.delete(`${BASE_URL}/books/${bookId}`);
      return response.data;

  } catch (error: any) {
      console.error(error);
      throw error;
  }
}

export const GetAll = async () => {
  try {

        const response = await axios.get(`${BASE_URL}/books`);
        return response;

  } catch (error: any) {
      console.error(error);
      throw error;
  }
};


