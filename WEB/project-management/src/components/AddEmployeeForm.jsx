import React, { Component } from 'react';
import {Button, FormGroup, FormControl} from 'react-bootstrap';
import "../styles/AddEmployee.css";

class AddEmployeeForm extends Component{
 
    constructor(props){
        super(props);
        this.state = {name: "", surname: "", patronymic: "",
                    email: "", roleIdIsValid: true};
 
        this.onSubmit = this.onSubmit.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
        this.onSurnameChange = this.onSurnameChange.bind(this);
        this.onPatronymicChange = this.onPatronymicChange.bind(this);
        this.onEmailChange = this.onEmailChange.bind(this);
    }
    validateSurname() {
        if (this.state.surname.length > 1) return 'success';
        if (this.state.surname.length > 0) return 'error';
        return null;
      }
      validateName() {
        if (this.state.name.length > 1) return 'success';
        if (this.state.name.length > 0) return 'error';
        return null;
      }
      validatePatronymic() {
        if (this.state.patronymic.length > 1) return 'success';
        if (this.state.patronymic.length > 0) return 'error';
        return null;
      }
    validateRole(roleId){
        return roleId>0 && roleId<4;
    }
    onNameChange(e) {
        this.setState({name: e.target.value});
    }
    onSurnameChange(e) {
        this.setState({surname: e.target.value});
    }
    onPatronymicChange(e) {
        this.setState({patronymic: e.target.value});
    }
    onEmailChange(e) {
        this.setState({email: e.target.value});
    }
    onSubmit(e) {
        e.preventDefault();
        var name = this.state.name.trim();
        var surname = this.state.surname.trim();
        var patronymic = this.state.patronymic.trim();
        var email = this.state.email.trim();
        var roleid = this.props.roleid;

        var roleValid = this.validateRole(roleid);
        this.setState({roleIdIsValid: roleValid});
        if (!name || !surname || !patronymic || !email || roleid<0 || roleid>3) {
            return;
        }
        if(this.state.roleIdIsValid===true){
                this.props.onEmployeeSubmit({ name: name, surname: surname,
                    patronymic: patronymic, email: email, roleId: roleid});
                    this.setState({name: "", surname: "", patronymic: "",
                    email: "", roleId: 0});
            }
    }
    render() {
        return (
          <form onSubmit={this.onSubmit} id="addemployee"> 
                <FormGroup controlId="formBasicText"
                    validationState={this.validateSurname()}>
                <FormControl
                    type="text"
                    placeholder="Фамилия"
                    value={this.state.surname}
                    onChange={this.onSurnameChange} />
                <FormControl.Feedback />
                </FormGroup>

                <FormGroup controlId="formBasicText"
                    validationState={this.validateName()}>
                <FormControl
                    type="text"
                    placeholder="Имя"
                    value={this.state.name}
                    onChange={this.onNameChange} />
                <FormControl.Feedback />
                </FormGroup>

                <FormGroup controlId="formBasicText"
                    validationState={this.validatePatronymic()}>
                <FormControl
                    type="text"
                    placeholder="Отчество"
                    value={this.state.patronymic}
                    onChange={this.onPatronymicChange} />
                <FormControl.Feedback />
                </FormGroup>

                <FormGroup controlId="formControlsEmail">
                <FormControl
                    type="email"
                    placeholder="E-mail"
                    value={this.state.email}
                    onChange={this.onEmailChange} />
                <FormControl.Feedback />
                </FormGroup>
            <Button type="submit" id="employee-form-btn">Добавить</Button>
          </form>
        );
    }
}

export default AddEmployeeForm;