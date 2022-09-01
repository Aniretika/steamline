import React, { useCallback, useEffect, useRef, useState } from 'react';
import { ILine } from '../models/line';
import { GetPositions } from '../api/agent';
import { Form, Formik } from 'formik';
import '../../formikStyles.css';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";
import '../../styles/lineRegStyle.css';
import { ILineForm } from '../models/lineForm';

function CreateLine() 
{
    const [queue, setQueue] = useState<ILine[]>([]);

    const fetchData = useCallback(async () => {
        GetPositions().then(function(result) {
            setQueue(result);
            console.log(result);
        })    
      }, [])
      
      useEffect(() => {
        fetchData()
          .catch(console.error);;
      }, [])

   
    return (
       <div className='main-register-block' style={{
            display:'flex',
            justifyContent: 'center',
            width: '60%',
            height: '100%'
        }}>
        <div className="main-table" style={{
            width: '100%',
            position: 'relative',
               }}>
            <Formik<ILineForm>
                onSubmit={(values) => { 
                    alert(JSON.stringify(values))
                } } 
                initialValues=
                {
                    {
                        login: "",
                        password: "",
                        name: "",
                        duration: "",
                        expireDate: undefined
                    }
                }
            >
                {({handleSubmit, values, handleChange, setFieldValue}) => {
                     let dateOnChange = (date: Date | null | undefined) => {
                        setFieldValue("expireDate", date)
                    }
                    return(
                    <Form className='line-register-form' onSubmit={handleSubmit}>
                        <h1 style={{ textAlign: "center", color: "white", fontSize: "50px" }}>Line Creator</h1>
                        <div className='form-accout'  style={{paddingLeft: '50px', float: 'right' }}> 
                            <h3 style={{ textAlign: "left", color: "white", fontSize: "25px" }}>Let`s register steam credentials</h3>
                            <label>Login</label>
                            <input 
                                type="text" 
                                name="login" 
                                placeholder='Type steam account login here'
                                value={values.login} onChange={handleChange}/>
                            <label>Password</label>
                            <input type="text" 
                                name="password" 
                                placeholder='Type steam account password here'
                                value={values.password} onChange={handleChange}/>
                        </div>
                        <div className='form-line' style={{paddingLeft: '50px' }}> 
                            <h3 style={{ textAlign: "left", color: "white", fontSize: "25px" }}>Line registration form</h3>
                            <label>Line`s name</label>
                            <input type="text" 
                                name="name" 
                                placeholder='Name'
                                value={values.name} onChange={handleChange}/>
                            <label>Duration for one command</label>
                            <input type="text" 
                                name="duration" 
                                placeholder='Date plugin here'
                                value={values.duration} onChange={handleChange}/>

                            <label>Expire time</label>
                            <DatePicker
                                name="expireDate" 
                                value={values.expireDate?.toLocaleDateString()}
                                selected={values.expireDate}
                                onChange={dateOnChange}/>
                            <button type="submit">Register line</button>
                        </div>
                    </Form>)
                }}
            </Formik>
           
            
        </div>
        </div>
    )
}
export default CreateLine;