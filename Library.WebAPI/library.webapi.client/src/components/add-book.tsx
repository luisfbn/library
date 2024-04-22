import React, { useState } from 'react';
import { TextField, Button, Paper, Container } from '@mui/material';
import BookService from '../api/book';

interface AddProps {
    isAdded: any;
    close: any;
}

const AddBook: React.FC<AddProps> = ({ isAdded, close }) => {

  const [title, setTitle] = useState<string>('');
  const [author, setAuthor] = useState<string>('');
  const [isLoading, setIsLoading] = useState<boolean>(false);

  
  const handleSubmit = async () => {
    setIsLoading(true);
    try {

        await BookService.AddBook({ title, author, isAvailable: true });
        setTitle('');
        setAuthor('');
        setIsLoading(false);
        isAdded();

    } catch (error) {
      console.error('Error adding book:', error);
      setIsLoading(false);
      alert('Error al agregar el libro');
    }
  };

  
  return (
    <Container component={Paper} maxWidth="xs" style={{ padding: '20px', marginTop: '50px' }}>
      <h2>Agregar Libro</h2>
      <TextField
        fullWidth
        label="Título"
        variant="outlined"
        margin="normal"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
      />
      <TextField
        fullWidth
        label="Autor"
        variant="outlined"
        margin="normal"
        value={author}
        onChange={(e) => setAuthor(e.target.value)}
      />

      <Button
        variant="contained"
        color="primary"
        fullWidth
        disabled={!title || !author || isLoading}
        onClick={handleSubmit}
        style={{ marginTop: '20px' }}
      >
        {isLoading ? 'Agregando...' : 'Agregar'}
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

export default AddBook;
