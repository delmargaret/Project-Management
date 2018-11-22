import React, { Component } from 'react';
import {Navbar, Nav, NavItem, NavDropdown, MenuItem} from 'react-bootstrap';

import * as tokenService from '../services/tokenService';
import * as method from '../services/methods';
import '../styles/Header.css';

class LogOutHeader extends Component {
    render() {
        return (
            <Navbar inverse >
                <Navbar.Header>
                    <Navbar.Brand >
                        <a href="/">Project Management</a>
                    </Navbar.Brand>
                </Navbar.Header>
                <Navbar.Collapse>
                    <Nav pullRight className="fixed">
                    <NavItem eventKey={1} href="/">
                        Log in
                    </NavItem>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        );
    }
}

class LogInHeader extends Component{
    constructor(props){
        super(props);
        this.state = {userName: " "}
    }
    componentWillMount(){
        tokenService.getUser().then(res =>{
            if(res.data!==""){
                this.setState({userName: JSON.parse(res.data).EmployeeName});
            }
        });
    }
    render(){
        if(!this.state.userName) return <Navbar inverse >
        <Navbar.Header>
            <Navbar.Brand >
                <a href="/">Project Management</a>
            </Navbar.Brand>
        </Navbar.Header>
    </Navbar>
        return <Navbar inverse >
        <Navbar.Header>
            <Navbar.Brand >
                <a href="/">Project Management</a>
            </Navbar.Brand>
        </Navbar.Header>
        <Navbar.Collapse>
            <Nav pullRight className="fixed">
                <NavDropdown title={this.state.userName} id="basic-nav-dropdown">
                    <MenuItem eventKey={1} href="/settings">Настройки</MenuItem>
                    <MenuItem divider />
                    <MenuItem eventKey={2} href="/" onSelect={() => this.props.onLogOut()}>Выход</MenuItem>
                </NavDropdown>
            </Nav>
        </Navbar.Collapse>  
    </Navbar>
    }
}

class Header extends Component{
    render(){
        var token = method.getToken();
        return token ? <LogInHeader onLogOut={this.props.onLogOut}/> : <LogOutHeader />;
    }
}

export default Header;