import './App.css';
import { BrowserRouter as Router } from 'react-router-dom'
import SidebarComponent from './Components/SidebarComponent';
import { TransactionTable } from './Components/TransactionTable';

function App() {
  return (
    <div>
      <Router>
        <SidebarComponent/>
        <TransactionTable/>
      </Router>
    </div>
  );
}

export default App;
