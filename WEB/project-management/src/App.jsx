import React, { Component } from 'react';
import './App.css';
import { BrowserRouter as Router, Route, Redirect } from "react-router-dom";
import AddProjectPage from "./components/AddProjectPage";
import AddEmployeePage from "./components/AddEmployeePage";
import ProjectManagerPage from "./components/ProjectManager";
import Header from "./components/Header";
import HomePage from "./components/HomePage";
import LogInPage from './components/LogInPage';
import RegistrationPage from './components/RegistrationPage';
import SettingsPage from './components/SettingsPage';
import * as method from './services/methods';
import * as tokenService from './services/tokenService';

class App extends Component {
  constructor(props){
    super(props);
    this.state = {isAuthorized: false, role: 0, employee: null}

    this.logIn = this.logIn.bind(this);
    this.logOut = this.logOut.bind(this);
}
  componentWillMount(){
      var token = method.getToken();
      if(token && !this.state.isAuthorized)
      {
        this.setState({
          isAuthorized: true
        });
      }
      else if (!token && this.state.isAuthorized) {
        this.setState({ isAuthorized: false });
    }
  }
  componentDidMount(){
    var token = method.getToken();
      if(token){
        tokenService.getUser().then(res =>{
          if(res!==null){
              this.setState({employee: res.data});
          }
      });
    }
  }
  logOut(){
    this.setState({isAuthorized: false});
    method.removeToken();
  }
  logIn(){
    this.setState({isAuthorized: true});
    tokenService.getRoleId().then(res =>{
      if(res!==null){
        this.setState({role: res.data});
      }
    });
    tokenService.getUser().then(res =>{
      if(res!==null){
          this.setState({employee: res.data});
      }
  });
    return <Redirect to="/home"/>;
  }
    render() {
      return (
        <Router>
          <div>
           <Header onLogOut={this.logOut}/>
            <Route exact path="/" render={() => (this.state.isAuthorized ? (
              <Redirect to="/home"/>) : (<LogInPage onLogIn={this.logIn} />))}/>
            <Route path="/home" component={HomePage} />
            <Route path="/staff" component={AddEmployeePage} />
            <Route path="/projects" component={AddProjectPage} />
            <Route path="/manager" component={ProjectManagerPage} />
            <Route path="/registration" component={RegistrationPage} />
            <Route path="/settings" component={SettingsPage} />
            <Route path="/login" render={() => (<LogInPage onLogIn={this.logIn} />)} />
        </div>
        </Router> 
      );
    }
  }
   
  export default App;
