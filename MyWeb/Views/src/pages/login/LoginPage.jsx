import React, {useState} from "react";
import api from "../../config/axios.js";
import {Link, useNavigate} from "react-router-dom";
import {
    Box,
    Button,
    Card, Checkbox, Container,
    CssBaseline,
    FormControl, FormControlLabel,
    FormLabel, Grid,
    InputLabel, Stack,
    TextField,
    Typography
} from "@mui/material";
import {set} from "@cloudinary/url-gen/actions/variable";
import {Form, Input} from "antd"
import { useForm } from "antd/es/form/Form";
import FormItem from "antd/es/form/FormItem/index.js";
import { styled } from '@mui/material/styles';
import MuiCard from '@mui/material/Card';
import {useDispatch} from "react-redux";
import {login} from "../../redux/userSlice.js";


function LoginPage() {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [formData, setFormData] = useState({
      email: "",
      password: ""});
    const [errors, setErrors] = useState({
        email: "",
        password: "",
    });
    const handleLogin = async (value) => {
      try {
          const response = await api.post("authentication/login", value);
          const {role, token} = response.data.loginResponse;
          localStorage.setItem("token", token);
          dispatch(login(response.data.loginResponse));
          if (role === "Customer"){
                navigate("/")
          }
          else if (role === "Manager"){
              navigate("/manager")
          }
          else if (role === "Staff"){
              navigate("/staff")
          }
          console.log(role, token);
      }catch (error){
          console.log(error.response);
      }
  }
    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));

        // Clear errors as user types
        setErrors((prev) => ({
            ...prev,
            [name]: "",
        }));
    };

    const validate = () => {
        const newErrors = {};
        if (!formData.email) newErrors.email = "Vui lòng nhập email!";
        if (!formData.password) newErrors.password = "Vui lòng nhập mật khẩu!";
        return newErrors;
    };

    const onSubmit = (e) => {
        e.preventDefault();
        const validationErrors = validate();
        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
        } else {
            handleLogin(formData);
        }
    };
    const SignInContainer = styled(Stack)(({ theme }) => ({
        height: 'calc((1 - var(--template-frame-height, 0)) * 100dvh)',
        minHeight: '100%',
        padding: theme.spacing(2),
        [theme.breakpoints.up('sm')]: {
            padding: theme.spacing(4),
        },
        '&::before': {
            content: '""',
            display: 'block',
            position: 'absolute',
            zIndex: -1,
            inset: 0,
            backgroundImage:
                'radial-gradient(ellipse at 50% 50%, hsl(210, 100%, 97%), hsl(0, 0%, 100%))',
            backgroundRepeat: 'no-repeat',
            ...theme.applyStyles('dark', {
                backgroundImage:
                    'radial-gradient(at 50% 50%, hsla(210, 100%, 16%, 0.5), hsl(220, 30%, 5%))',
            }),
        },
    }));

    const Card = styled(MuiCard)(({ theme }) => ({
        display: 'flex',
        flexDirection: 'column',
        alignSelf: 'center',
        width: '100%',
        padding: theme.spacing(4),
        gap: theme.spacing(2),
        margin: 'auto',
        [theme.breakpoints.up('sm')]: {
            maxWidth: '450px',
        },
        boxShadow:
            'hsla(220, 30%, 5%, 0.05) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.05) 0px 15px 35px -5px',
        ...theme.applyStyles('dark', {
            boxShadow:
                'hsla(220, 30%, 5%, 0.5) 0px 5px 15px 0px, hsla(220, 25%, 10%, 0.08) 0px 15px 35px -5px',
        }),
    }));

    return (
        <SignInContainer>

        <Card variant={"outlined"}>
        <Box
            component="form"
            onSubmit={onSubmit}
            sx={{
                display: "flex",
                flexDirection: "column",
                gap: 2,
                maxWidth: 400,
                margin: "0 auto",
            }}
        >
            <Typography variant="h4" align="center" gutterBottom>
                Đăng nhập
            </Typography>
            <TextField
                label="Email"
                name="email"
                value={formData.email}
                onChange={handleChange}
                error={!!errors.email}
                helperText={errors.email}
                fullWidth
            />
            <TextField
                label="Mật khẩu"
                name="password"
                type="password"
                value={formData.password}
                onChange={handleChange}
                error={!!errors.password}
                helperText={errors.password}
                fullWidth
            />
            <Button
                type="submit"
                variant="contained"
                color="primary"
                fullWidth
                className="login-button"
            >
                Login
            </Button>
        </Box>
        </Card>
        </SignInContainer>

    );
}

export default LoginPage