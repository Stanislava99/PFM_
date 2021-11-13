import './App.css';
import { BrowserRouter as Router } from 'react-router-dom'
import SidebarComponent from './Components/SidebarComponent';

function App() {
  return (
    <div>
      <Router>
        <SidebarComponent/>
      </Router>
    </div>
  );
}

export default App;
