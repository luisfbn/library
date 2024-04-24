import axios from 'axios';

// const BASE_URL = 'http://localhost:5190/api';
const BASE_URL = 'https://localhost:7120/api';



  export const RegisterLoan = async (bookId: number, readerId: number) => {
    try {

      const token = localStorage.getItem('token');
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}` 

      const url = `${BASE_URL}/loan/register`;
      const dataForm = { bookId: bookId, readerId: readerId };
      const config = { headers: { 'Authorization': 'Bearer ' + token } };

      const response = await axios.post(url, dataForm, config);

      return response.data;


    } catch (error: any) {

      if (error.response && error.response.status === 401) {
        throw new Error('Esta acción requere un usario autenticado');
      }

      console.error('Error:', error);
      throw error;
    }
  }

  export const ReturnBook = async (bookId: number) => {
    try {
        const token = localStorage.getItem('token');
        const url = `${BASE_URL}/loan/return/${bookId}`;
        const config = { headers: { 'Authorization': 'Bearer ' + token } };

        const response = await axios.post(url, {}, config);
        return response.data;

    } catch (error: any) {

        if (error?.response && error.response.status === 401) {
          throw new Error('Esta acción requere un usario autenticado');
        }

        console.error('Error:', error);
        throw error;
    }
  }

