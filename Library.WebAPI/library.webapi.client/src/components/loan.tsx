import React, { useEffect, useState } from 'react';
import { Button, Paper, Container, InputLabel, Select, MenuItem } from '@mui/material';

import * as LoanService from '../api/loan';
import * as ReaderService from '../api/reader';

interface LoanProps {
  book: {
    id: number;
    title: string;
    author: string;
    isAvailable: boolean;
  };
  userId?: number;
  close: any;
  isLended: any;
}

const Loan: React.FC<LoanProps> = ({ book, userId = 1, close, isLended }) => {
  const [readerId, setReaderId] = useState<number>(-1);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [readers, setReaders] = useState<any[]>([]); 


  useEffect(() => {
    setIsLoading(true);
    const fetchReaders = async () => {
      try {
        const data = await ReaderService.GetAll();
        setReaders(data);
      } catch (error) {
        console.error('Error fetching readers:', error);
      }
      finally {
        setIsLoading(false);
      }
    };

    fetchReaders();
  }, []);

  
  const handleSubmit = async () => {
    setIsLoading(true);

    try {
      await LoanService.RegisterLoan(book.id, readerId);
      setReaderId(userId);
      isLended();
    } catch (error) {
      console.error('Error registering loan:', error);
      alert('Error al registrar el préstamo');
    }
    finally {
      setIsLoading(false);
    }

  };

  return (
    <Container component={Paper} maxWidth="xs" style={{ padding: '20px', marginTop: '50px' }}>
      <h2>Registrar Préstamo</h2>
      <p>Libro: {book.title} - {book.author}</p>
     
      <InputLabel id="reader-label">Lector</InputLabel>
      {readers && <>
        <Select
            labelId="reader-label"
            fullWidth
            value={readerId}
            onChange={(e) => setReaderId(e.target.value as number)}
            variant="outlined"
        >
            <MenuItem key={-1} value={-1}>
                Seleccione
            </MenuItem>
            {readers.map((reader) => (
            <MenuItem key={reader.id} value={reader.id}>
                {reader.name}
            </MenuItem>
            ))}
        </Select>
      </>}
      
      <Button
        variant="contained"
        color="primary"
        fullWidth
        disabled={!readerId || isLoading || readerId<0}
        onClick={handleSubmit}
        style={{ marginTop: '20px' }}
      >
        {isLoading ? 'Registrando...' : 'Registrar'}
      </Button>
      
      <Button
        variant="text"
        fullWidth
        disabled={isLoading}
        onClick={() => close()}
        style={{ marginTop: '20px' }}
      >
        {isLoading ? '...' : 'Cerrar'}
      </Button>
    </Container>
  );
};

export default Loan;
