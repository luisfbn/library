import React, { useEffect, useState } from 'react';
import { TextField, Button, Typography, Container, Modal, Paper } from '@mui/material';
import * as UserService from '../api/user';

const User: React.FC = () => {

  const [user, setUser] = useState<any>(null);
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

  const [openModalLogin, setOpenModalLogin] = useState(false);

  useEffect(() => {
    
    const user = UserService.get();
    if (user) {
        setIsLoggedIn(true);
        setUser(user);
    }else {
        setIsLoggedIn(false);
        setUser(null);
    }

  }, []);

  const handleLogin = async () => {
    try {
      const data = await UserService.authenticate(username, password);
      setUser(data);
      setIsLoggedIn(true);
      setOpenModalLogin(false);
    } catch (error) {
      console.error('Error:', error);
      alert('Authentication failed');
    }
  };

  const handleLogout = () => {
    UserService.logout();
    setIsLoggedIn(false);
    setUser(null);
  };

  
  const handleOpenLogin = () => {
    setOpenModalLogin(true);
    setUsername("");
    setPassword("");
  };

  return (
    <Container component="main" maxWidth="xs" sx={{mb: 4, mt: 4}}>
      <div>
        {!isLoggedIn ? (<>
            <Button
              variant="text"
              color="secondary"
              onClick={handleOpenLogin}
            >
              Iniciar Sesión
            </Button>
            </>
          
        ) : (
          <div>
            <Typography variant="h6">{`Hola, ${user?.username}!`}</Typography>
            <Button
              fullWidth
              variant="outlined"
              color="secondary"
              onClick={handleLogout}
            >
              Desconectarse
            </Button>
          </div>
        )}
      </div>

      
        <Modal open={openModalLogin} onClose={() => setOpenModalLogin(false)}>
            <Container component={Paper} maxWidth="xs" style={{ padding: '20px', marginTop: '50px' }}>
            <Typography variant="h5" style={{textAlign: "center"}}>
                Autenticarse
            </Typography>
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              id="username"
              label="Usuario"
              name="username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="password"
              label="Contraseña"
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <Button
              fullWidth
              variant="contained"
              color="primary"
              sx={{mt: 2}}
              onClick={handleLogin}
              disabled={!username || !password}
            >
              Iniciar Sesión
            </Button>
            
            <Button
              fullWidth
              variant="outlined"
              color="primary"
              sx={{mt: 2}}
              onClick={() => setOpenModalLogin(false)}
            >
              Cerrar
            </Button>
            </Container>
        </Modal> 
    </Container>

    
  );
};

export default User;
