import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import activityApi from "api/activityAPI"; 


const order = createSlice({
    name: "activities",
    initialState: { loading: false, activities: [], adminActivityId: null },
    reducers: {
		searchActivity: (state, action) => {
            state.activities = action.payload 
		},
		setAdminActivity: (state, action) => {
            state.adminActivityId = action.payload 
		},
	},
    extraReducers: builder => {
        builder
        .addCase(fetchActivities.pending, (state, action) => {
            state.loading = true;
        })
        .addCase(fetchActivities.fulfilled, (state, action) => {
            state.loading = false;
            state.activities = action.payload;
        })
        .addCase(fetchActivities.rejected, (state, action) => {
            state.loading = false;
            state.activities = [];
        })
    }
})

const { reducer, actions } = order;
export const { searchActivity, setAdminActivity } = actions;
export default reducer;

export const fetchActivities = createAsyncThunk('activities/fetchActivities', async (params) => {
    const res = await activityApi.getActivityOfAdmin(params);
    return res
})