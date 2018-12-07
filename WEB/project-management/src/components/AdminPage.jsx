import React, { Component } from 'react';
import {Button} from 'react-bootstrap';
import '../styles/AdminHomePage.css';
import Menu from './Menu';
import {Grid, Row, Col} from 'react-bootstrap';


class AdminPage extends Component{
      render(){
          return(
            <Grid>
            <Row>
              <Col xs={3} md={3}>{<Menu/>}</Col>
              <Col xs={15} md={9}>
              <div className="span10" id="homepage">
              <h1>Добро пожаловать!</h1>
                  <div id="buttons">
                    <div className="button" id="staffButton">
                        <Button bsSize="large" href="/staff">Добавить сотрудника</Button>
                    </div>
                    <div className="button" id="projectsButton">
                        <Button bsSize="large" href="/projects">Добавить проект</Button>
                    </div>
                  </div>
              </div>
              </Col>
            </Row>
          </Grid>
          );
      };
}

export default AdminPage;