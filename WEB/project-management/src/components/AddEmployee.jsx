import React, { Component } from 'react';
import Modal from 'react-modal';

class EmployeeList extends Component{
    render(){ 
        return <div>
                    <table>
                        <tbody>
                        {
                    this.props.emp.map((employee) => {  
                        var data = JSON.parse(employee); 
                        var id = data.Id;  
                        return <tr key={id}>
                            <td>{data.EmployeeSurname}</td>
                            <td>{data.EmployeeName}</td>
                            <td>{data.EmployeePatronymic}</td>
                            <td>{data.Email}</td>
                            <td>{data.RoleId}</td>
                        </tr>              
                    })
                    }
                        </tbody>
                    </table>
                </div>
            }
}

class AddEmployeeForm extends Component{
 
    constructor(props){
        super(props);
        this.state = {name: "", surname: "", patronymic: "",
                    email: "", nameIsValid: true, surnameIsValid: true, 
                    patronymicIsValid: true, roleIdIsValid: true};
 
        this.onSubmit = this.onSubmit.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
        this.onSurnameChange = this.onSurnameChange.bind(this);
        this.onPatronymicChange = this.onPatronymicChange.bind(this);
        this.onEmailChange = this.onEmailChange.bind(this);
    }
    validateName(name){
        return name.length>=2;
    }
    validateSurname(surname){
        return surname.length>=2;
    }
    validatePatronymic(patronymic){
        return patronymic.length>=2;
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

        var nameValid = this.validateName(name);
        this.setState({nameIsValid: nameValid});
        var surnameValid = this.validateSurname(surname);
        this.setState({surnameIsValid: surnameValid});
        var patronymicValid = this.validatePatronymic(patronymic);
        this.setState({patronymicIsValid: patronymicValid});
        var roleValid = this.validateRole(roleid);
        this.setState({roleIdIsValid: roleValid});
        if (!name || !surname || !patronymic || !email || roleid<0 || roleid>3) {
            return;
        }
        if(this.state.nameIsValid===true && this.state.surnameIsValid===true && 
            this.state.patronymicIsValid===true && this.state.roleIdIsValid===true){
                this.props.onEmployeeSubmit({ name: name, surname: surname,
                    patronymic: patronymic, email: email, roleId: roleid});
                    this.setState({name: "", surname: "", patronymic: "",
                    email: "", roleId: 0});
            }
    }
    render() {
        var nameError = this.state.nameIsValid===true?"":" * Введите имя";
        var surnameError = this.state.surnameIsValid===true?"":" * Введите фамилию";
        var patronymicError = this.state.patronymicIsValid===true?"":" * Введите отчество";
        return (
          <form onSubmit={this.onSubmit}>
              <p>
                  <input type="text"
                         placeholder="Фамилия"
                         value={this.state.surname}
                         onChange={this.onSurnameChange} />
                    <label>{surnameError}</label>
              </p>
              <p>
                  <input type="text"
                         placeholder="Имя"
                         value={this.state.name}
                         onChange={this.onNameChange} />
                    <label>{nameError}</label>
              </p>
              <p>
                  <input type="text"
                         placeholder="Отчество"
                         value={this.state.patronymic}
                         onChange={this.onPatronymicChange} />
                    <label>{patronymicError}</label>
              </p>
              <p>
                  <input type="email"
                         placeholder="E-mail"
                         value={this.state.email}
                         onChange={this.onEmailChange} />
              </p>
            <input type="submit" value="Добавить" />
          </form>
        );
    }
}
 
const customStyles = {
    content : {
      top                   : '50%',
      left                  : '50%',
      right                 : 'auto',
      bottom                : 'auto',
      marginRight           : '-50%',
      transform             : 'translate(-50%, -50%)'
    }
  };
  
Modal.setAppElement(document.getElementById('root'));

class RoleList extends Component{
 
    constructor(props){
        super(props);
        this.state = { roles: [], activeRole: 0, employees: [], modalIsOpen: false};
        this.onAddEmployee = this.onAddEmployee.bind(this);
        this.onClick = this.onClick.bind(this);
        this.openModal = this.openModal.bind(this);
        this.afterOpenModal = this.afterOpenModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
    }
    openModal() {
        this.setState({modalIsOpen: true});
      }
    afterOpenModal() {

      }
    closeModal() {
        this.setState({modalIsOpen: false});
        this.loadEmployees();
      }
    loadRoles() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", "http://localhost:12124/api/Role/GetRoles", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ roles: data });
        }.bind(this);
        xhr.send();
    }
    loadEmployees() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", "http://localhost:12124/api/Employee/GetEmployees", true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ employees: data });
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadRoles();
        this.loadEmployees();
    }
    onAddEmployee(employee) {
        if (employee) {
 
            var data = JSON.stringify({"EmployeeName":employee.name, "EmployeeSurname":employee.surname,
        "EmployeePatronymic": employee.patronymic, "Email": employee.email, "RoleId": employee.roleId});
            fetch("http://localhost:12124/api/Employee/CreateEmployee", {
                method: 'POST', 
                body: data,
                type: "json",
                headers: {"Content-Type": "application/json"}
            });
        }
    }
    onClick(id){
        this.setState({
            activeRole: id
        })
        this.openModal();
    }
    render(){ 
        const activerole = this.state.activeRole;
        const roledata = this.state.roles;
        const employeedata = this.state.employees;
        if (!roledata) return <div>Загрузка...</div>; 
        return <div>
                <h2>Добавить сотрудника</h2>
                <div>
                    <table>
                        <tbody>
                        {
                    this.state.roles.map((role) => {  
                        var data = JSON.parse(role); 
                        var id = data.Id;  
                        return <tr key={id}>
                            <td>{data.Id}.</td>
                            <td>{data.RoleName}</td>
                            <td><button onClick={() => this.onClick(id)}>
                                Добавить</button>
                            </td>
                        </tr>              
                    })
                    }
                        </tbody>
                    </table>
                    <EmployeeList emp={employeedata}/>
                </div>
                <Modal
                    isOpen={this.state.modalIsOpen}
                    onAfterOpen={this.afterOpenModal}
                    onRequestClose={this.closeModal}
                    style={customStyles}
                    contentLabel="Example Modal">
                    <button onClick={this.closeModal}>close</button>
                    <AddEmployeeForm roleid = {activerole} onEmployeeSubmit={this.onAddEmployee}/>
        </Modal>
        </div>;
     }
}

export default RoleList;
