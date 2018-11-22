import React, { Component } from 'react';
import {Nav, NavItem} from 'react-bootstrap';
import '../styles/Menu.css'

class Menu extends Component{

      render(){
          return(
            <Nav bsStyle="pills" stacked activeKey={1} id="navmenu">
            <NavItem eventKey={2} href="/staff">
              Сотрудники
            </NavItem>
            <NavItem eventKey={3} href="/projects">
              Проекты
            </NavItem>
            <NavItem eventKey={4} href="/manager">
              Менеджер проектов
            </NavItem>
          </Nav>
          );
      };
}

export default Menu;
