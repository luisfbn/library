import React, { useState, useEffect } from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Modal, Typography, Tooltip, Backdrop, CircularProgress } from '@mui/material';
import { AssignmentReturn, Delete, LibraryAdd, LibraryBooks } from '@mui/icons-material';
import Swal from 'sweetalert2';

import AddBook from './add-book';
import Loan from './loan';
import * as BookService from '../api/book';
import * as LoanService from '../api/loan';

const Books: React.FC = () => {

  const [books, setBooks] = useState<any[]>([]);
  const [selectedBook, setSelectedBook] = useState<any | null>(null);
  const [openModalAdd, setOpenModalAdd] = useState(false);
  const [openModalLoan, setOpenModalLoan] = useState(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    fetchBooks();
  }, []);

  const fetchBooks = async () => {
    try {
        setIsLoading(true);
        const { data } = await BookService.GetAllBooks();
        
        setBooks(data);
    } catch (error) {
        console.error('Error fetching books:', error);
    }
    finally {
        setIsLoading(false);
    }
  };

  const handleAddedBook = () => {
    setOpenModalAdd(false);
    fetchBooks();
    Swal.fire('Agregado', 'El libro ha sido agregado correctamente.', 'success');
  }

  
  const handleLendedBook = () => {
    setOpenModalLoan(false);
    fetchBooks();
    Swal.fire('Prestado', 'El libro ha sido prestado correctamente.', 'success');
  }

  const handleDeleteBook = async (bookId: number, title: string) => {
    Swal.fire({
        title: '¿Estás seguro?',
        text: `Esta acción eliminará el libro "${title}" permanentemente.`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar',
      }).then(async (result) => {
        if (result.isConfirmed) {
          try {
            const result = await BookService.Delete(bookId.toString());
            fetchBooks();
            if (result) {
                const updatedBooks = books.filter((book) => book.id !== bookId);
                setBooks(updatedBooks);
                Swal.fire('Eliminado', 'El libro ha sido eliminado correctamente.', 'success');
            }
          } catch (error) {
            console.error('Error deleting book:', error);
            Swal.fire('Error', 'Ha ocurrido un error al eliminar el libro.', 'error');
          }
        }
      });
      
  };

  const handleReturnBook = async (book: any) => {

    Swal.fire({
      title: '¿Estás seguro?',
      text: `Desea regresar el libro ${book.title}.`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sí, marcar como devuelto',
      cancelButtonText: 'Cancelar',
    }).then(async (result) => {
      if (result.isConfirmed) {
        try {

          await LoanService.ReturnBook(book.id);
          fetchBooks();
          Swal.fire('Devuelto', 'El libro ha sido marcado como devuelto correctamente.', 'success');

        } catch (error: any) {
          
            Swal.fire({
              titleText: 'Sin Acceso', 
              text: error.message, 
              icon: 'warning', 
              target: document.getElementById('form-modal')
            });

        }
      }
    });
  };
  

  const handleOpenModal = (book: any) => {
    setSelectedBook(book);
    setOpenModalLoan(true);
  };

  const handleCloseModal = () => {
    setSelectedBook(null);
    setOpenModalLoan(false);
  };

  return (<>
    <Typography variant='h5' sx={{mb: 3}}>
        Listado de Libros
    </Typography>   

    <Tooltip title="Agregar Libro">
        <Button 
            variant="contained" 
            onClick={() => setOpenModalAdd(true)}
            style={{ marginLeft: '10px', marginBottom: 50 }}
            >
            <LibraryAdd style={{ marginRight: '10px' }} /> Agregar Libro
        </Button>
    </Tooltip>

    <TableContainer component={Paper} style={{
        // minWidth: 1200, 
        maxWidth: 1200
        }}>
    <Table>
        <TableHead>
        <TableRow>
            {/* <TableCell>Id</TableCell> */}
            <TableCell style={{fontWeight: "bold"}}>Título</TableCell>
            <TableCell style={{fontWeight: "bold"}}>Autor</TableCell>
            <TableCell style={{fontWeight: "bold"}}>Disponible</TableCell>
            <TableCell style={{fontWeight: "bold", textAlign: "center"}}>Acciones</TableCell>
        </TableRow>
        </TableHead>
        <TableBody>
        {books.map((book) => (
            <TableRow key={book.id}>
            {/* <TableCell>{book.id}</TableCell> */}
            <TableCell>{book.title}</TableCell>
            <TableCell>{book.author}</TableCell>
            <TableCell>{book.isAvailable ? 'Sí' : 'No'}</TableCell>
            <TableCell>
                {book.isAvailable && <>
                    <Tooltip title="Eliminar">
                        <Button 
                        variant="outlined" 
                        onClick={() => handleDeleteBook(book.id, book.title)}
                        data-testid={`delete-button-${book.id}`} 
                        >
                            <Delete />
                        </Button>
                    </Tooltip>
                </>}
                {book.isAvailable 
                ? <Tooltip title="Prestar Libro">
                    <Button 
                        variant="contained" 
                        color="primary" 
                        onClick={() => handleOpenModal(book)}
                        style={{ marginLeft: '10px' }}
                        >
                        <LibraryBooks />
                    </Button>
                  </Tooltip>
                : <Tooltip title="Regresar Libro">
                    <Button 
                        variant="contained" 
                        color="primary" 
                        onClick={() => handleReturnBook(book)}
                        >
                        <AssignmentReturn />
                    </Button>
                  </Tooltip>
                  }
            </TableCell>
            </TableRow>
        ))}
        </TableBody>
    </Table>
    </TableContainer>


    <Modal open={openModalAdd} onClose={() => setOpenModalAdd(false)}>
        <AddBook isAdded={handleAddedBook} close={() => setOpenModalAdd(false)} />
    </Modal> 

    <Modal open={openModalLoan} onClose={() => handleCloseModal()}>
        <Loan book={selectedBook} isLended={handleLendedBook} userId={1} close={() => setOpenModalLoan(false)} />
    </Modal> 


    <Backdrop
      sx={{ color: '#fff' }}
      open={isLoading}
    >
      <CircularProgress color="inherit" />
    </Backdrop>
    
</>);
};

export default Books;
