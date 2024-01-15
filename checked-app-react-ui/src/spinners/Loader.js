// import Spinner from 'react-bootstrap/Spinner';
import '../styles/loader.css'
// import { Audio } from 'react-loader-spinner'

function Loader(props) {
// return <Audio/>;
return <span class="loader"></span>
// return (
//     <div className='overlay'>
//         <div className='loader'>{props.children}</div>
//     </div>
// )
}

export default Loader;