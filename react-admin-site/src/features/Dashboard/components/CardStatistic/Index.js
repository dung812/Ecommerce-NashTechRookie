import React from 'react';
import { Card } from 'react-bootstrap';
import CountUp from 'react-countup';


const CardStatistic = (props) => {
    const { title, number, isUp, percent, prefix, suffix } = props;

    return (
        <React.Fragment>
            <Card>
                <Card.Body>
                    <h6 className='mb-3'>{title}</h6>
                    <h3 className='mb-3'>
                        <CountUp 
                            start={0}
                            end={number}
                            duration={1.75}
                            decimals={3}
                            decimal=","
                            prefix={prefix}
                            suffix={suffix}
                        />
                    </h3>
                    <span className={`${isUp? 'text-success' : 'text-danger'} me-2`}>
                        {isUp ? <i className='bx bx-upvote'></i> : <i className='bx bx-downvote' ></i>}
                        {percent}
                    </span>
                    <span>Since last month</span>
                </Card.Body>
            </Card>
        </React.Fragment>
    );
};


export default CardStatistic;
