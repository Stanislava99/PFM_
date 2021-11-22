import './App.css';
import { BrowserRouter as Router,Route, Switch } from 'react-router-dom'
import SidebarComponent from './Components/SidebarComponent';
import { TransactionTable } from './Components/TransactionTable';
import {Container, Row, Col } from "react-bootstrap";


function App() {
  return (
    <div>
      <Router>
        <Container fluid >
          <Row >
            <Col xs={2} id="sidebar-wrapper" >
               <SidebarComponent/>
            </Col>
            <Col xs={10} >
              <Switch>
                <Route path="/" component={TransactionTable}></Route>
                <Route path="/transactions" component={TransactionTable}></Route>
              </Switch>
            </Col>
          </Row>
        
        </Container>
       
      </Router>
    </div>
  );
}

export default App;
