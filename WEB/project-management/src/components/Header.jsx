import React, { Component } from 'react';
import {Navbar} from 'react-bootstrap';

class Header extends Component {
    render() {
        return (
            <Navbar inverse >
                <Navbar.Header>
                    <Navbar.Brand >
                        <a href="/">Project Management</a>
                    </Navbar.Brand>
                </Navbar.Header>
            </Navbar>
        );
    }
}

export default Header;